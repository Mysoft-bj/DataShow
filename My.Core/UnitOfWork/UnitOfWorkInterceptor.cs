using System.Threading.Tasks;

using Castle.DynamicProxy;


namespace My.Core.UnitOfWork
{
    /// <summary>
    /// This interceptor is used to manage database connection and transactions.
    /// </summary>
    public class UnitOfWorkInterceptor : IInterceptor
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWorkInterceptor(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

       
        public void Intercept(IInvocation invocation)
        {
            if (_unitOfWorkManager.Current != null)
            {
                //Continue with current uow
                invocation.Proceed();
                return;
            }
            var method = invocation.MethodInvocationTarget;
            var attrs =invocation.Method.GetCustomAttributes(typeof(UnitOfWorkAttribute), true);
            if (attrs.Length ==0)
            {
                //No need to a uow
                invocation.Proceed();
                return;
            }

            using (var uow = _unitOfWorkManager.Begin())
            {
                invocation.Proceed();
                uow.Complete();
            }
        }

        
    }
}