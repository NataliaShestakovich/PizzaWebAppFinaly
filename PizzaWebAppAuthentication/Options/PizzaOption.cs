using static System.Net.Mime.MediaTypeNames;

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
        public string ErrorCreatingPizza { get; set; } //"Не заполнены все параметры для создания пиццы"
        public string SuccessUpdatePizzaInDatabase { get; set; } //"Пицца с названием {0} была изменена в базе данных"
        public string ErrorUpdatePizzaInDatabase { get; set; } //"Произошла ошибка. Пицца с названием {0} не была изменена в базе данных"
        public string StandartPizzaBase { get; set; } // "стандартная"
        public string CustomPizzaName { get; set; } //  "Клиентская: "
        public string SuccessDeletePizzaFromDatabase { get; set; } //"Пицца с названием {0} была удалена из базы данных"
        public string ErrorDeletePizzaFromDatabase { get; set; } //"Произошла ошибка. Пицца с названием {0} не была удалена из базы данных"
        public string EmptyCart { get; set; } // "Ваша корзина пуста, добавьте пиццу в корзину"
        public string SuccessAcceptOrder { get; set; } // "Ваш заказ принят"
        public string ErrorAcceptOrder { get; set; } // "Неполные данные для заказа"
        public string ErrorAddOrderToDatabase { get; set; } // "Заказ не принят в обработку"
        public string IngredientDuplicationError { get; set; } // "Ингредиент с названием {0} уже существует в базе данных"
        public string SuccessAddIngredientToDatabase { get; set; } //"Ингредиент с названием {0} добавлен в базу данных"
        public string ErrorAddIngredientToDatabase { get; set; } //"Произошла ошибка. Ингредиент с названием {0} не добавлен в базу данных"
        public string SuccessUpdateIngredientInDatabase { get; set; } //"Ингредиент с названием {0} был изменен в базе данных"
        public string ErrorUpdateIngredientInDatabase { get; set; } //"Произошла ошибка. Ингредиент с названием {0} не был изменен в базе данных"
        public string SuccessDeleteIngredientFromDatabase { get; set; } //"Ингредиент с названием {0} был удален из базы данных"
        public string ErrorDeleteIngredientFromDatabase { get; set; } //"Произошла ошибка. Ингредиент с названием {0} не был удален из базы данных"
        public string ErrorExistUserInDatabase { get; set; } // Пользователь не прошел регистрацию
        public string ErrorGetOrderData { get; set; } // "Произошла ошибка при получении данных заказа"
        public string ErrorFindObject { get; set; } // "Запрашиваемый объект не найден в базе данных"




    }
}

   
