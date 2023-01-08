//Реализовать базу данных игроков и методы для работы с ней.
//У игрока может быть уникальный номер, ник, уровень, флаг – забанен ли он(флаг - bool).
//Реализовать возможность добавления игрока, бана игрока по уникальный номеру, разбана игрока 
//по уникальный номеру и удаление игрока.
//Создание самой БД не требуется, задание выполняется инструментами, которые вы уже изучили 
//в рамках курса. Но нужен класс, который содержит игроков и её можно назвать "База данных". 

using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace PlayerDB
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Player player = new Player();
			DataBase db = new DataBase();

			string choice;

			while (Console.ReadKey().Key!=ConsoleKey.Spacebar)
			{
				Console.WriteLine(" 1 - Добавить игрока\n " +
					"2 - удалить игрока\n " +
					"3 - показать игроков\n " +
					"4 - забанить/разбанить игрока по id\n " +
					"5 - Информация об игроке \n Для выхода нажмите Space");

				switch (choice = Console.ReadLine())
				{
					case "1":
						db.AddPlayer();
						break;
					case "2":
						db.DeletePlayer();
						break;
					case "3":
						db.ShowDB();
						break;
					case "4":
						db.ChangeBanStatus();
						break;
					case "5":
						db.PlayerInfo();
						break;
					default:
						break;
				}

				Console.ReadKey();
				Console.Clear();
			}
		}
	}

	public class DataBase
	{
		List<Player> players = new List<Player>();

		public void AddPlayer()
		{
			int id = players.Count();

			Console.WriteLine("Введите имя персонажа");
			string name = Console.ReadLine();

			Console.WriteLine("Введите уровень персонажа");
			int lvl = Convert.ToInt32(Console.ReadLine());

			Player player = new Player { Id = id, Name = name, Lvl = lvl };

			players.Add(player);
		}

		public void DeletePlayer()
		{
			Console.WriteLine("Введите id игрока");
			int id = Convert.ToInt32(Console.ReadLine());

			foreach (var item in players)
			{
				if (item.Id==id)
				{
					players.Remove(item);
					break;
				}
			}
		}

		public void ChangeBanStatus()
		{
			Console.WriteLine("Введите id игрока для бана/разбана");
			int id = Convert.ToInt32(Console.ReadLine());

			foreach (var item in players)
			{
				if (item.Id==id)
				{
					item.Banned = !item.Banned;
					if (item.Banned)
					{
						Console.WriteLine("Игрок забанен");
					}
					else
					{
						Console.WriteLine("Игрок разбанен");
					}
				}
			}
		}

		public void PlayerInfo()
		{
			Console.WriteLine("Введите id игрока информацию о котором хотите узнать");
			int id = Convert.ToInt32(Console.ReadLine());

			foreach (var item in players)
			{
				if (item.Id==id)
				{
					item.ShowInfo();
				}
			}
		}

		public void ShowDB()
		{
			foreach (var item in players)
			{
				item.ShowInfo();
			}
		}
	}

	public class Player
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Lvl { get; set; }
		public bool Banned { get; set; }

		public void ShowInfo()
		{
			Console.WriteLine($"Имя игрока: {Name}\n	" +
				$"Id: {Id}\n	" +
				$"Уровень: {Lvl}\n	" +
				$"Игрок забанен? {Banned}\n ");
		}
	}
}