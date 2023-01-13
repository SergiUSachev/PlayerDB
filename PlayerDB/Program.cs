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
			DataBase dataBase = new DataBase();

			const string CommandAddPlayer = "1";
			const string CommandShowDataBase = "3";
			const string CommandDeletePlayer = "2";
			const string CommandBanStatus = "4";
			const string CommandPlayerInfo = "5";
			const string CommandExit = "exit";

			while (Console.ReadLine()!=CommandExit)
			{
				Console.WriteLine(
					$"{CommandAddPlayer} - Добавить игрока\n " +
					$"{CommandShowDataBase} - удалить игрока\n " +
					$"{CommandDeletePlayer} - показать игроков\n " +
					$"{CommandBanStatus} - забанить/разбанить игрока по id\n " +
					$"{CommandPlayerInfo} - Информация об игроке \n Для выхода введите {CommandExit}"
					);

				string command = Console.ReadLine();

				switch (command)
				{
					case CommandAddPlayer:
						dataBase.AddPlayer();
						break;
					case CommandDeletePlayer:
						dataBase.DeletePlayer();
						break;
					case CommandShowDataBase:
						dataBase.ShowAllPlayersInfo();
						break;
					case CommandBanStatus:
						dataBase.ChangeBanStatus();
						break;
					case CommandPlayerInfo:
						dataBase.ShowPlayerInfo();
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

			if (_players.Count()>1)
			{
				player.Id = _players[_players.IndexOf(_players.Last())-1].Id+1;
			}

			if (_players.Count()==1)
			{
				player.Id = _players[_players.IndexOf(_players.Last())].Id+1;
			}
		}

		public void DeletePlayer()
		{
			Player player;
			if (TryGetPlayer(out player))
			{
				_players.Remove(player);
			}
		}

		public void ChangeBanStatus()
		{
			Player player;
			if (TryGetPlayer(out player))
			{
				if (player.IsBanned)
				{
					UnBanPlayer();
					Console.WriteLine("Игрок разбанен");
				}
				else
				{
					BanPlayer();
					Console.WriteLine("Игрок забанен");
				}
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

		public void UnBanPlayer()
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

			player = null;
			Console.WriteLine("Такого игрока нет");
			return false;
		}
	}

	public class Player
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