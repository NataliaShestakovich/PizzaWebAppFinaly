namespace PizzaWebAppAuthentication.Repositories
{
    public interface IRepository <T1, T2> where T1 : class 
                                          where T2: struct
    {
        void Update(T1 objectT);
        void Update(T2 objectT);

        void Delete(T1 objectT);
        void Delete(T2 objectT);

        T1? Get(Guid id);

        List<T1> GetList();

    }
}
