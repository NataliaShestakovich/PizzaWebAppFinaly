namespace PizzaWebAppAuthentication.Repositories
{
    public interface IRepository <T1, T2>
    {
        void Add (T1)
        
        T1? Get(T2 id);

        List<T1> GetList();

        void Update(T1 objectT);
       
        void Delete(T1 objectT);       

    }
}
