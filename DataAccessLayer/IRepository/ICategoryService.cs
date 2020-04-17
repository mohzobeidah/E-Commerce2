using DataBaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.IRepository
{
   public interface ICategoryService: IRepository<Category>
    {


         IQueryable<Category> GetPaginated(string filter, int initalPage, int pageSize, out int totalRecord, out int recordsFilter);
        
    }
}
