using System.Collections.Generic;

namespace Lab1
{
    sealed class BusinessLogic
    {
        readonly DataLogic _dataLogic = new DataLogic() {};

    /*    public bool[] Selection(string companyName)
        {
            int length = _dataLogic.DBLength;
            bool[] selected = new bool[length];
            for (int i = 0; i < length; i++)
                if (_dataLogic.Read(i).CompanyName == companyName)
                    selected[i] = true;
            return selected;
        }*/

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

        public int DBLength => _dataLogic.DBLength;

       // public Task Read(int id) => _dataLogic.Read(id);

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
