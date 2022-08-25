using ProcessPensionDetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionDetails.Repository.IRepository
{
    public interface IProcessDetailsRepository
    {
        Task<List<processDetails>> GetListAsync(string adharNumber,string token);

        processDetails CalculatePension(processDetails pensionerDetail);
        Task SaveDataAsync(string token);
        Task Update(processDetails pensionerDetail);
        processDetails GetPensionerDetailFromDB(string aadharNumber);
    }
}
