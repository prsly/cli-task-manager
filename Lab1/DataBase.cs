using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab1
{
    [Serializable]
    sealed class Task
    {
        public string TaskName { get; set; }
        public string TaskStatus { get; set; } 
        public int TaskHours { get; set; }
        public Task(string taskName, string taskStatus, int taskHours)
        {
            TaskName = taskName;
            TaskStatus = taskStatus;
            TaskHours = taskHours;
        }
    }

    //-База данных – список записей БД;
    [Serializable]
    class DataBase //односвязный список строк бд
    {
        [Serializable]
        private class TaskList 
        {
            private TaskList _next; //ссылка на следующий элемент в списке
            private Task _element;
            
            public Task Element
            {
                get { return _element; }
                set { _element = value; }
            }

            public TaskList Next
            {
                get { return _next; }
                set { _next = value; }
            }

        }

        public int Length { get; private set; }
        private TaskList _head;
        private TaskList _tail;

        public DataBase()
        {
            // создание пустого списка
            _head = null;
            _tail = _head;
            Length = 0;
        }

        public void Push(Task element)
        {
            if (_head == null)
            {
                // создать узел, сделать его головным
                _head = new TaskList {Element = element};
                // этот же узел и является хвостовым
                _tail = _head;
                // следующего узла нет
                _head.Next = null;
            }
            else
            {
                // создать временный узел
                TaskList tempBL = new TaskList();
                // следующий за предыдущим хвостовым узлом - это наш временный новый узел
                _tail.Next = tempBL;
                // сделать его же новым хвостовым
                _tail = tempBL;
                _tail.Element = element;
                // следующего узла пока нет
                _tail.Next = null;
            }
            ++Length;
        }

        public void Delete(int position)
        {
            if (position == 0)
            {
                _head = _head.Next;
            }
            else
            {
                TaskList tempBL = _head;
                TaskList prev = _head;
                for (int i = 0; i < position + 1; ++i)
                {
                    if (position > 0 && i == position - 1)
                        prev = tempBL;
                    tempBL = tempBL.Next;
                }
                prev.Next = tempBL;
            }
            Length--;
            GC.Collect();
        }

        public Task this[int position]
        {
            get
            {
                if (position >= 0)
                {
                    TaskList tempBL = _head;
                    for (int i = 0; i < position; ++i)
                    {
                        // переходим к следующему узлу списка
                        tempBL = tempBL.Next;
                    }
                    return tempBL.Element;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (position >= 0)
                {
                    TaskList tempBL = _head;
                    for (int i = 0; i < position; ++i)
                        tempBL = tempBL.Next;
                    tempBL.Element = value;
                }
            }
        }

    }

    //-Манипуляция с файлом, хранилищем БД
    //-Непосредственная манипуляция с файлом-хранилищем БД осуществляется специальным классом
    sealed class DataBaseFile
    {
        public DataBase MainDataBase = new DataBase() {};

        public bool Save(string file)
        {
            try
            {
                BinaryFormatter binFormat = new BinaryFormatter();
                using (Stream fStream = new FileStream(file + ".dat", FileMode.Create, FileAccess.Write))
                    binFormat.Serialize(fStream, MainDataBase);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Load(string file)
        {
            try
            {
                BinaryFormatter binFormat = new BinaryFormatter();
                using (FileStream fStream = new FileStream(file + ".dat", FileMode.Open, FileAccess.Read))
                    MainDataBase = (DataBase) binFormat.Deserialize(fStream);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
