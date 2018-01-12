using System;

using NHibernate;
using NHibernate.Type;
using My.Core;
using My.Domain;

namespace My.NHibernate.Interceptors
{
    internal class NHibernateInterceptor : EmptyInterceptor
    {
      //  public IEntityChangeEventHelper EntityChangeEventHelper { get; set; }

       


        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            //Set CreationTime for new entity
            //if (entity is IUpdater)
            //{
            //    for (var i = 0; i < propertyNames.Length; i++)
            //    {
            //        if (propertyNames[i] == "CreationTime")
            //        {
            //            state[i] = (entity as IUpdater).UpdatedTime = Clock.Now;
            //        }
            //    }
            //}

            ////Set CreatorUserId for new entity
            //if (entity is ICreationAudited)
            //{
            //    for (var i = 0; i < propertyNames.Length; i++)
            //    {
            //        if (propertyNames[i] == "CreatorUserId")
            //        {
            //            state[i] = (entity as ICreationAudited).CreatorUserId = _abpSession.Value.UserId;
            //        }
            //    }
            //}

         //   EntityChangeEventHelper.TriggerEntityCreatingEvent(entity);
        //    EntityChangeEventHelper.TriggerEntityCreatedEventOnUowCompleted(entity);

            return base.OnSave(entity, id, state, propertyNames, types);
        }

        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types)
        {
           

            //Set modification audits
            if (entity is IUpdater)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "UpdatedTime")
                    {
                        currentState[i] = (entity as IUpdater).UpdatedTime = DateTime.Now;
                    }
                    if (propertyNames[i] == "UpdatedId")
                    {
                    }
                }
            }            
            return base.OnFlushDirty(entity, id, currentState, previousState, propertyNames, types);
        }

        public override void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            //EntityChangeEventHelper.TriggerEntityDeletingEvent(entity);
            //EntityChangeEventHelper.TriggerEntityDeletedEventOnUowCompleted(entity);

            base.OnDelete(entity, id, state, propertyNames, types);
        }
    }
}
