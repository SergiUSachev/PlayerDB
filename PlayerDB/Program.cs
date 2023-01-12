//Реализовать базу данных игроков и методы для работы с ней.
//У игрока может быть уникальный номер, ник, уровень, флаг – забанен ли он(флаг - bool).
//Реализовать возможность добавления игрока, бана игрока по уникальный номеру, разбана игрока 
//по уникальный номеру и удаление игрока.
//Создание самой БД не требуется, задание выполняется инструментами, которые вы уже изучили 
//в рамках курса. Но нужен класс, который содержит игроков и её можно назвать "База данных". 

using System.Linq;
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
			DataBase dataBase = new DataBase();

			const string commandAddPlayer = "1";
			const string commandShowDataBase = "3";
			const string commandDeletePlayer = "2";
			const string commandBanStatus = "4";
			const string commandPlayerInfo = "5";
			const string commandExit = "exit";

			while (Console.ReadLine()!=commandExit)
			{
				Console.WriteLine(" 1 - Добавить игрока\n " +
					"2 - удалить игрока\n " +
					"3 - показать игроков\n " +
					"4 - забанить/разбанить игрока по id\n " +
					"5 - Информация об игроке \n Для выхода введите exit");
				string command = Console.ReadLine();

				switch (command)
				{
					case commandAddPlayer:
						dataBase.AddPlayer();
						break;
					case commandDeletePlayer:
						dataBase.DeletePlayer();
						break;
					case commandShowDataBase:
						dataBase.ShowDB();
						break;
					case commandBanStatus:
						dataBase.ChangeBanStatus();
						break;
					case commandPlayerInfo:
						dataBase.PlayerInfo();
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
		private List<Player> _players = new List<Player>();

		public void AddPlayer()
		{
			int id = _players.Count();
			int level;

			Console.WriteLine("Введите имя персонажа");
			string name = Console.ReadLine();

			Console.WriteLine("Введите уровень персонажа");
			bool tryReadLevel = int.TryParse(Console.ReadLine(), out level);

			while (tryReadLevel==false)
			{
				Console.WriteLine("Ошибка! Введите уровень персонажа ОДНИМ ЧИСЛОМ");
				tryReadLevel = int.TryParse(Console.ReadLine(), out level);
			}

			Player player = new Player { Id = id, Name = name, Level = level };

			_players.Add(player);

			bool noDoubledIdFlag = false;



		}

		public void DeletePlayer()
		{
			int id;
			Console.WriteLine("Введите id игрока");
			bool tryReadId = int.TryParse(Console.ReadLine(), out id);

			foreach (var item in _players)
			{
				if (item.Id==id)
				{
					_players.Remove(item);
					break;
				}
			}
		}

		public void ChangeBanStatus()
		{
			int id;
			Console.WriteLine("Введите id игрока для бана/разбана");
			bool tryReadId = int.TryParse(Console.ReadLine(), out id);

			foreach (var item in _players)
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
			int id;
			Console.WriteLine("Введите id игрока информацию о котором хотите узнать");
			bool tryReadId = int.TryParse(Console.ReadLine(), out id);

			foreach (var player in _players)
			{
				if (player.Id==id)
				{
					player.ShowInfo();
				}
			}
		}

		public void ShowDB()
		{

			foreach (var player in _players)
			{
				player.ShowInfo();
			}
		}

		public bool TryGetPlayer(int id, out Player player)
		{
			for (int i = 0; i < _players.Count; i++)
			{
				if (id == _players[i].Id)
				{
					player = _players[i];
					return true;
				}
			}
			player = null;
			return false;
		}
	}

		public class Player
		{
			public Player(int id, string name, int level, bool banned)
			{
				Id=id;
				Name=name;
				Level=level;
				Banned=banned;
			}
			public Player()
			{

			}
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! get set бессмыслица
			public int Id { get; set; }
			public string Name { get; set; }
			public int Level { get; set; }
			public bool Banned { get; set; }

			public void ShowInfo()
			{
				Console.WriteLine($"Имя игрока: {Name}\n	" +
					$"Id: {Id}\n	" +
					$"Уровень: {Level}\n	" +
					$"Игрок забанен? {Banned}\n ");
			}
		}
	
}