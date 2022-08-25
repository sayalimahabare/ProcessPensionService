
using Newtonsoft.Json;
using ProcessPensionDetails.Data;
using ProcessPensionDetails.Models;
using ProcessPensionDetails.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProcessPensionDetails.Repository
{
    public class ProcessDetailsRepository : IProcessDetailsRepository
    {
        private readonly ApplicationDbContext _db;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProcessDetailsRepository));

        public ProcessDetailsRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        List<string> publicBanks = new List<string>() { 
        "SBI","BOI","PNB"
        };

        List<string> privateBanks = new List<string>() {
        "HDFC","ICICI","AXIS","HFBC"
        };
        public async Task<List<processDetails>> GetListAsync(string adharNumber,string token)
        {
            List<processDetails> pensionerDetail = new List<processDetails>();
            using (var client = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44356/api/pensioner/GetDetailsByAdhar/" + adharNumber))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = await client.SendAsync(requestMessage);

                    // var response = await client.GetAsync("https://localhost:44356/api/pensioner/GetDetailsByAdhar/"+adharNumber);

                    int sc = (int)response.StatusCode;
                    if (sc == 200)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        pensionerDetail = JsonConvert.DeserializeObject<List<processDetails>>(apiResponse);

                    }

                    else
                    {           
                        return null;
                    }
                }
            }
            _log4net.Info(" Pensioner Detail returned From GetListAsync method of: " + nameof(ProcessDetailsRepository));
            return pensionerDetail;


            //Console.WriteLine(result.StatusCode);
        }

        public processDetails CalculatePension(processDetails pensionerDetail)
        {
            if (pensionerDetail.SelfOrFamilyPension == "self")
            {
                pensionerDetail.pensionAmount = pensionerDetail.SalaryEarned * 0.08 + pensionerDetail.Allowances;
               
            }
            else
            {
                pensionerDetail.pensionAmount = pensionerDetail.SalaryEarned * 0.05 + pensionerDetail.Allowances;
               
            }
            if (publicBanks.Contains(pensionerDetail.BankName.ToUpper()))
            {
                pensionerDetail.serviceCharge = 500;
            }
            else if (privateBanks.Contains(pensionerDetail.BankName.ToUpper()))
            {
                pensionerDetail.serviceCharge = 550;
            }
            return pensionerDetail;
        }

        public async Task SaveDataAsync(string token)
        {
            List<processDetails> pensionerDetails = new List<processDetails>();
            using (var client = new HttpClient())
           {
            using(var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44356/api/pensioner/GetDetails"))
                {
                    requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                    var response = await client.SendAsync(requestMessage);

                    int sc = (int)response.StatusCode;
                    if (sc == 200)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        pensionerDetails = JsonConvert.DeserializeObject<List<processDetails>>(apiResponse);
                    }
                }
                        
            }
            foreach (var entry in pensionerDetails)
            {
                var penDetail = _db.PensionerDetails.FirstOrDefault(item => item.AdharNumber == entry.AdharNumber);
                if (penDetail == null)
                {
                    _db.PensionerDetails.Add(entry);
                }
            }
            _db.SaveChanges();
        }


        public async Task Update(processDetails pensionerDetail)
        {
            var penDetail = _db.PensionerDetails.FirstOrDefault(item => item.AdharNumber == pensionerDetail.AdharNumber);
            penDetail.pensionAmount = pensionerDetail.pensionAmount;
            penDetail.serviceCharge = pensionerDetail.serviceCharge;
            penDetail.AdharNumber = pensionerDetail.AdharNumber;
            penDetail.Name = pensionerDetail.Name;
            penDetail.DOB = pensionerDetail.DOB;
            penDetail.PAN = pensionerDetail.PAN;
            penDetail.SalaryEarned = pensionerDetail.SalaryEarned;
            penDetail.Allowances = pensionerDetail.Allowances;
            penDetail.SelfOrFamilyPension = pensionerDetail.SelfOrFamilyPension;
            penDetail.BankName = pensionerDetail.BankName;
            penDetail.AccountNumber = pensionerDetail.AccountNumber;
            penDetail.PublicOrPrivate = pensionerDetail.PublicOrPrivate;

            await _db.SaveChangesAsync();


        }


        public processDetails GetPensionerDetailFromDB(string aadharNumber)
        {
            return _db.PensionerDetails.FirstOrDefault(item => item.AdharNumber == aadharNumber);
        }
    }
}
