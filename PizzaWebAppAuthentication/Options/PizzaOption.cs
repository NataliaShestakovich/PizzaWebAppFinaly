namespace PizzaWebAppAuthentication.Options
{
    public class PizzaOption
    {
        public const string SectionName = "PizzaOptions";

        public string SuccessAddCustomPizza { get; set; }//"Пицца собранная вами {0} добавлена в корзину покупок"
        public string SuccessAddPizzaInCart { get; set; }//"Пицца {0} добавлена в корзину покупок"
        public string ErrorAddPizzaInCart { get; set; }//"Пицца не была добавлена в корзину покупок"
        public string SuccessDecreasePizzaInCart { get; set; }//"Количество пиццы {0} было уменьшено в корзине покупок"
        public string SuccessRemovePizzaFromCart { get; set; }//"Пицца {0} была удалена из корзины покупок"
        public string SuccessAddPizzaToDatabase { get; set; } //"Пицца с названием {0} добавлена в базу данных"
        public string ErrorAddPizzaToDatabase { get; set; } //"Произошла ошибка. Пицца с именем {0} не добавлена в базу данных"
        public string ErrorAddInDatabase { get; set; } //"Пицца с названием {0} уже существует в базе данных"
        public string ErrorAddingIngredients { get; set; } //"Не добавлен ни один ингрединет"
        public string SuccessUpdatePizzaInDatabase { get; set; } //"Пицца с названием {0} была изменена в базе данных"
        public string ErrorUpdatePizzaInDatabase { get; set; } //"Произошла ошибка. Пицца с названием {0} не была изменена в базе данных"
        public string StandartPizzaBase { get; set; } // "стандартная"
        public string SuccessDeletePizzaFromDatabase { get; set; } //"Пицца с названием {0} была удалена из базы данных"
        public string ErrorDeletePizzaFromDatabase { get; set; } //"Произошла ошибка. Пицца с названием {0} не была удалена из базы данных"




    }
}

   
