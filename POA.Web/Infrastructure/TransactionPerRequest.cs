using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POA.Domain;
using POA.Web.Infrastructure.Tasks;
using System.Data;
using System.Data.Entity;

namespace POA.Web.Infrastructure
{
    public class TransactionPerRequest: IRunOnEachRequest, IRunOnError, IRunAfterEachRequest
    {
        private readonly POADBEntities _context;
        private readonly HttpContextBase _httpContext;

        public TransactionPerRequest(POADBEntities context, HttpContextBase httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
         void IRunOnEachRequest.Execute()
        {
            _httpContext.Items["_Transaction"] = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        void IRunOnError.Execute()
        {
            _httpContext.Items["_Error"] = true;
        }

        void IRunAfterEachRequest.Execute()
        {
            var transaction = (DbContextTransaction)_httpContext.Items["_Transaction"];
            if(_httpContext.Items["_Error"] != null)
            {
                transaction.Rollback();
            }
            else
            {
                transaction.Commit();
            }
            
        }
    }
}