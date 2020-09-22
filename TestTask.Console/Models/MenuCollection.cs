using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TestTask.Console.Models
{
    public class MenuCollection<T> : IEnumerable<T>
    {
        private readonly List<T> _menu;

        public MenuCollection(params T[] menuItems)
        {
            _menu = new List<T>(menuItems);
        }

        public void Add(T menuItem)
        {
            _menu.Add(menuItem);
        }


        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _menu.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _menu.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            foreach (var menuItem in _menu)
            {
                array.SetValue(menuItem, index++);
            }
        }

        public bool Any(Func<T, bool> predicate)
        {
            return _menu.Any(predicate);
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return _menu.FirstOrDefault(predicate);
        }

        public int Count => _menu.Count();
        public bool IsSynchronized => false;
        public object SyncRoot => this;

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _menu.Select(x => x.ToString()));
        }
    }
}