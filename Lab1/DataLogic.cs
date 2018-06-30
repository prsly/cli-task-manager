using System.Collections.Generic;

namespace Lab1
{
    sealed class DataLogic
    {
        private readonly DataBaseFile _dataBaseFile = new DataBaseFile();

        public void Create(Task task)
        {
            _dataBaseFile.MainDataBase.Push(task);
        }

        List<Task> db = new List<Task>();
        public List<Task> ReadAll()
        {
            for (int i = 0; i < _dataBaseFile.MainDataBase.Length; i++)
            {
                db.Add(_dataBaseFile.MainDataBase[i]);
            }
            return db;
        }

        public bool Update(int id, Task task)
        {
            _dataBaseFile.MainDataBase[id] = task;
            return true;
        }

        public void Delete(int id)
        {
            _dataBaseFile.MainDataBase.Delete(id);
        }

        public int GetDBLength()
        {
            return _dataBaseFile.MainDataBase.Length;
        }

        public bool LoadFile(string file)
        {
            return _dataBaseFile.Load(file);
        }

        public bool SaveFile(string file)
        {
            return _dataBaseFile.Save(file);
        }
    }
}