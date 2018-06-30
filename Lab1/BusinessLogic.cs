using System.Collections.Generic;

namespace Lab1
{
    sealed class BusinessLogic
    {
        readonly DataLogic _dataLogic = new DataLogic() {};

        internal void Add(Task task)
        {
            _dataLogic.Create(task);
        }

        public void Edit(int id, Task task)
        {
            _dataLogic.Update(id, task);
        }

        public void Delete(int id)
        {
            _dataLogic.Delete(id);
        }

        public int DBLength
        {
            get
            {
                return _dataLogic.GetDBLength();
            }
        }

        public List<Task> ReadAll()
        {
            return _dataLogic.ReadAll();
        }

        public bool LoadFile(string file)
        {
            return _dataLogic.LoadFile(file);
        }

        public bool SaveFile(string file)
        {
            return _dataLogic.SaveFile(file);
        }

    }
}
