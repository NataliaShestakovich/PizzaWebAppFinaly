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



    }
}
