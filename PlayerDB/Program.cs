//Реализовать базу данных игроков и методы для работы с ней.
//У игрока может быть уникальный номер, ник, уровень, флаг – забанен ли он(флаг - bool).
//Реализовать возможность добавления игрока, бана игрока по уникальный номеру, разбана игрока 
//по уникальный номеру и удаление игрока.
//Создание самой БД не требуется, задание выполняется инструментами, которые вы уже изучили 
//в рамках курса. Но нужен класс, который содержит игроков и её можно назвать "База данных". 

using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace PlayerDB
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Database database = new Database();

			const string CommandAddPlayer = "1";
			const string CommandDeletePlayer = "2";
			const string CommandShowDataBase = "3";
			const string CommandBan = "4";
			const string CommandUnban = "5";
			const string CommandPlayerInfo = "6";
			const string CommandExit = "exit";

			bool isCommandExit = false;

			while (isCommandExit==false)
			{
				Console.WriteLine(
					$"{CommandAddPlayer} - Добавить игрока\n" +
					$"{CommandDeletePlayer} - удалить игрока\n" +
					$"{CommandShowDataBase} - показать игроков\n" +
					$"{CommandBan} - забанить игрока по id\n" +
					$"{CommandUnban} - разбанить игрока по id\n" +
					$"{CommandPlayerInfo} - Информация об игроке \nДля выхода введите {CommandExit}\n" +
					$"Введите номер команды и нажмите Enter"
					);

				string command = Console.ReadLine();

				switch (command)
				{
					case CommandAddPlayer:
						database.AddPlayer();
						break;

					case CommandDeletePlayer:
						database.DeletePlayer();
						break;

					case CommandShowDataBase:
						database.ShowAllPlayersInfo();
						break;

					case CommandUnban:
						database.UnbanPlayer();
						break;

					case CommandPlayerInfo:
						database.ShowPlayerInfo();
						break;

					case CommandBan:
						database.BanPlayer();
						break;

					case CommandExit:
						isCommandExit = true;
						break;

					default:
						Console.WriteLine("Ошибка ввода команды");
						break;
				}

				Console.WriteLine("Нажмите любую кнопку для продолжения");
				Console.ReadKey();
				Console.Clear();
			}
		}
	}

	class Database
	{
		private List<Player> _players = new List<Player>();
		private int _lastId;

		public void AddPlayer()
		{
			int id = _lastId++;
			_lastId = id;
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
		}

		public void DeletePlayer()
		{
			Player player;
			if (TryGetPlayer(out player))
			{
				_players.Remove(player);
				Console.WriteLine("Игрок удалён");
			}
		}

		public void BanPlayer()
		{
			Player player;

			if (TryGetPlayer(out player))
			{
				player.IsBanned = true;
			}
		}

		public void UnbanPlayer()
		{
			Player player;

			if (TryGetPlayer(out player))
			{
				player.IsBanned = false;
			}
		}

		public void ShowPlayerInfo()
		{
			Player player;

			if (TryGetPlayer(out player))
			{
				player.ShowInfo();
			}
		}

		public void ShowAllPlayersInfo()
		{
			foreach (var player in _players)
			{
				player.ShowInfo();
			}
		}

		private bool TryGetPlayer(out Player player)
		{
			int id;
			Console.WriteLine("Введите id игрока");
			bool tryReadId = int.TryParse(Console.ReadLine(), out id);

			while (tryReadId==false)
			{
				Console.WriteLine("Ошибка! Введите id персонажа ОДНИМ ЧИСЛОМ");
				tryReadId = int.TryParse(Console.ReadLine(), out id);
			}

			for (int i = 0; i < _players.Count; i++)
			{
				if (id == _players[i].Id)
				{
					player = _players[i];
					return true;
				}
			}

			Console.WriteLine("Такого игрока нет");
			player = null;
			return false;
		}
	}

	class Player
	{
		public int Id;
		public string Name;
		public int Level;
		public bool IsBanned;

		public Player(int id, string name, int level, bool isBanned)
		{
			Id=id;
			Name=name;
			Level=level;
			IsBanned=isBanned;
		}

		public Player()
		{

		}

		public void ShowInfo()
		{
			Console.WriteLine($"Имя игрока: {Name}\n	" +
				$"Id: {Id}\n	" +
				$"Уровень: {Level}\n	" +
				$"Игрок забанен? {IsBanned}\n ");
		}
	}
}