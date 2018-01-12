using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Core;
using My.Domain;
using My.Core.Sql;
namespace My.Application
{
   
  public abstract  class BaseService:IService
    {
      private Repository _repo;
      private IMyDB _myDB;
      protected Repository Repo
      {
          get
          {
              if (_repo == null)
              {
                  _repo = IocManager.Resolve<Repository>();
              }
              return _repo;
          }
      }
      protected IMyDB MyDB
      {
          get
          {
              if (_myDB == null)
              {
                  _myDB =  IocManager.Resolve<IMyDB>();
              }
              return _myDB;
          }
      }
      
      public BaseService(){
         
        
      }
     



  //  Mysoft.Core.ReflectionHelper
    }
}
