namespace My.Domain
{
   
    public interface ISoftDelete
    {      
        bool IsDeleted { get; set; }
    }
}
