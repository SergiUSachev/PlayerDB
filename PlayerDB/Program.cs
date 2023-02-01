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
			const string CommandAddPlayer = "1";
			const string CommandDeletePlayer = "2";
			const string CommandShowDataBase = "3";
			const string CommandBan = "4";
			const string CommandUnban = "5";
			const string CommandPlayerInfo = "6";
			const string CommandExit = "exit";

			Database database = new Database();

			bool isCommandExit = false;

			while (isCommandExit==false)
			{
				Console.WriteLine(
					$"{CommandAddPlayer} - добавить игрока\n" +
					$"{CommandDeletePlayer} - удалить игрока\n" +
					$"{CommandShowDataBase} - показать игроков\n" +
					$"{CommandBan} - забанить игрока по id\n" +
					$"{CommandUnban} - разбанить игрока по id\n" +
					$"{CommandPlayerInfo} - информация об игроке \nдля выхода введите {CommandExit}\n" +
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
		private int _lastId = 0;

		public void AddPlayer()
		{
			int id = _lastId++;
			int level;
			bool isBanned;

			Console.WriteLine("Введите имя персонажа");
			string name = Console.ReadLine();

			level = GetLevel();
			isBanned = false;

			_players.Add(new Player(id, name, level, isBanned));
		}

		public void DeletePlayer()
		{
			if (TryGetPlayer(out Player player))
			{
				_players.Remove(player);
				Console.WriteLine("Игрок удалён");
			}
		}

		public void BanPlayer()
		{
			if (TryGetPlayer(out Player player))
			{
				player.BanPlayer();
			}
		}

		public void UnbanPlayer()
		{
			if (TryGetPlayer(out Player player))
			{
				player.UnbanPlayer();
			}
		}

		public void ShowPlayerInfo()
		{
			if (TryGetPlayer(out Player player))
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

		private int GetLevel()
		{
			int level = 0;
			bool isLevelReadable = false;

			while (isLevelReadable==false)
			{
				Console.WriteLine("Введите уровень персонажа ОДНИМ ПОЛОЖИТЕЛЬНЫМ ЧИСЛОМ");
				isLevelReadable = int.TryParse(Console.ReadLine(), out level);

				if (level<0)
				{
					isLevelReadable = false;
				}
			}

			return level;
		}
	}

	class Player
	{
		private string _name;
		private int _level;
		private bool _isBanned;

		public Player(int id, string name, int level, bool isBanned)
		{
			Id=id;
			_name=name;
			_level=level;
			_isBanned=isBanned;
		}

		public Player()
		{

		}

		public int Id { get; private set; }

		public void SetId(int id)
		{
			Id = id;
		}

		public void SetName(string name)
		{
			_name = name;
		}

		public void SetLevel(int level)
		{
			_level = level;
		}

		public void BanPlayer()
		{
			_isBanned = true;
		}

		public void UnbanPlayer()
		{
			_isBanned = false;
		}

		public void ShowInfo()
		{
			Console.WriteLine($"Имя игрока: {_name}\n	" +
				$"Id: {Id}\n	" +
				$"Уровень: {_level}\n	" +
				$"Игрок забанен? {_isBanned}\n ");
		}
	}
}