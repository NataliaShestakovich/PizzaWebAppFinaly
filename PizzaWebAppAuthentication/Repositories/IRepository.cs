namespace PizzaWebAppAuthentication.Repositories
{
    public interface IRepository <T1, T2>
    {
        void Add(T1 entity);
        
        T1? Get(T2 fieldname);

        List<T1> GetList();

        void Update(T1 objectT);
       
        void Delete(T1 objectT);       

    }
}
