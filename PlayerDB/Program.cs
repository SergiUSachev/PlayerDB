//Реализовать базу данных игроков и методы для работы с ней.
//У игрока может быть уникальный номер, ник, уровень, флаг – забанен ли он(флаг - bool).
//Реализовать возможность добавления игрока, бана игрока по уникальный номеру, разбана игрока 
//по уникальный номеру и удаление игрока.
//Создание самой БД не требуется, задание выполняется инструментами, которые вы уже изучили 
//в рамках курса. Но нужен класс, который содержит игроков и её можно назвать "База данных". 

namespace PlayerDB
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DataBase database[] = new DataBase({ });
		}
	}

	public class DataBase
	{
		private int _id;
		private string _name;
		private int _lvl;
		private bool _banned;

		public DataBase(int id, string name, int lvl, bool banned)
		{
			_id = id;
			_name = name;
			_lvl = lvl;
			_banned = banned;

		}

		public void ShowInfo()	
		{
			Console.WriteLine($"id игрока: {_id}\nНик игрока:{_name}\nУровень игрока:{_lvl}\n");
			if (_banned)
			{
				Console.WriteLine("Игрок забанен :( ");
			}
		}
	}
}