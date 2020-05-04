using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Entity;

namespace FoodModel
{
    public class DataManager
    {
        public List<Food> GetAll()
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                List<Food> foods = foodDBEntities.Foods.ToList();
                return foods;
            }
        }

        public Food GetByID(int id)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                Food food = foodDBEntities.Foods.ToList().Find(f => f.ID == id) as Food;

                return food;
            }
        }

        public void Add(Food food)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                foodDBEntities.Foods.Add(food);
                foodDBEntities.SaveChanges();
            }
        }

        public void Update(Food food)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                Food currentFood = foodDBEntities.Foods.SingleOrDefault(f => f.ID == food.ID);

                if (currentFood != null)
                {
                    currentFood.Name = food.Name;
                    currentFood.Ingridients = food.Ingridients;
                    currentFood.Calories = food.Calories;
                    currentFood.Grade = food.Grade;
                }

                foodDBEntities.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                Food food = foodDBEntities.Foods.ToList().Find(f => f.ID == id);
                foodDBEntities.Foods.Remove(food);
                foodDBEntities.SaveChanges();
            }
        }

        public Food GetByName(string name)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                Food food = foodDBEntities.Foods.FirstOrDefault(f => f.Name.ToUpper().Contains(name.ToUpper()));
                return food;
            }
        }

        public List<Food> GetByMinCal(int minCal)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                List<Food> foods = (from f in foodDBEntities.Foods where f.Calories > minCal select f).ToList();
                return foods;
            }
        }

        public List<Food> GetByCategories(string name, int grade, int minCal, int maxCal)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                List<Food> foods = foodDBEntities.Foods.ToList();
                List<Food> filteredOutFoods = new List<Food>();

                foreach(Food food in foods)
                {
                    if (!String.IsNullOrEmpty(name))
                    {
                        if (!food.Name.ToUpper().Contains(name.ToUpper()))
                            filteredOutFoods.Add(food);
                    }

                    if (grade != 0)
                    {
                        if (food.Grade != grade)
                            filteredOutFoods.Add(food);
                    }

                    if (minCal != 0)
                    {
                        if (food.Calories < minCal)
                            filteredOutFoods.Add(food);
                    }

                    if (maxCal != 0)
                    {
                        if (food.Calories > maxCal)
                            filteredOutFoods.Add(food);
                    }
                }

                foreach (Food food in filteredOutFoods)
                {
                    foods.Remove(food);
                }

                return foods;
            }
        }

        public void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
