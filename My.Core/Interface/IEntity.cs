using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using My.Core;
using System.ComponentModel;
namespace My.Domain
{
  

    public interface IEntity {
   
    }

   
    [Serializable]
    public abstract class Entity : IEntity
    {   
       // [Key]       
       // public virtual Guid Id { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is IEntity))
            {
                return false;
            }


            if (ReferenceEquals(this, obj))
            {
                return true;
            }


            var other = (Entity)obj;



            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.IsAssignableFrom(typeOfOther) && !typeOfOther.IsAssignableFrom(typeOfThis))
            {
                return false;
            }
            var Id = GetId();
            return Id.Equals(other.GetId());
        }     
        public override int GetHashCode()
        {
            return GetId().GetHashCode();
        }    
        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }       
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }      
        public override string ToString()
        {
            return this.ToJson();
        }
        public object GetId() {
            var meta = MetaDataHelper.GetMetaData(this.GetType());
           return this.GetValue(meta.KeyColumn.ColumnName);
         
        }
        //public static implicit operator string(Entity entity)
        //{
        //    return entity.ToJson();
        //}
    }

   public interface ITreeNode<T> : ITreeNode {
       IList<T> Childen { get; set; }
   }
   public interface ITreeNode : IEntity
   {
       Guid ParentId { get; set; }
       int Sort { get; set; }
       string TreePath { get; set; }
      
   }
   public interface IUpdater : IEntity
   {
       [Display(Name = "修改时间")]
       DateTime UpdatedTime { get; set; }
        [Display(Name = "修改人")]
       Guid UpdatedId { get; set; }
   }
 
  
  

 

}
