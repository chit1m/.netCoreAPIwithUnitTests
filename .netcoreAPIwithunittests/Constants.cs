using System.ComponentModel.DataAnnotations;

namespace WebsiteCustomers
{
    public static class Constants
    {
        public static string NoCustomersFound = "Customers not found";
        public enum CategoryEnum
        {
            [Display(Name = "Computer Store")]
            ComputerStore,
            [Display(Name = "Corporate")]
            Corporate,
            [Display(Name = "Supermarket")]
            Supermarket,
            [Display(Name = "Novelty Shop")]
            NoveltyShop,
            [Display(Name = "Gift Store")]
            GiftStore
        }
    }
}
