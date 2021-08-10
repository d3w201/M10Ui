using System.Collections.Generic;

namespace Script.Utils
{
    public class CircularList<T> : List<T>
    {
        private int _currentIndex = 0;

        public int CurrentIndex
        {
            get
            {
                if (_currentIndex > Count - 1)
                {
                    _currentIndex = 0;
                }

                if (_currentIndex < 0)
                {
                    _currentIndex = Count - 1;
                }

                return _currentIndex;
            }
            set => _currentIndex = value;
        }

        public int NextIndex
        {
            get
            {
                if (_currentIndex == Count - 1) return 0;
                return _currentIndex + 1;
            }
        }

        public int PreviousIndex
        {
            get
            {
                if (_currentIndex == 0) return Count - 1;
                return _currentIndex - 1;
            }
        }

        public T Next => Count > 0 ? this[NextIndex] : default;

        public T Previous => Count > 0 ? this[PreviousIndex] : default;

        public T MoveNext
        {
            get
            {
                if (Count == 0) return default;
                _currentIndex++;
                return this[CurrentIndex];
            }
        }

        public T MovePrevious
        {
            get
            {
                if (Count == 0) return default;
                _currentIndex--;
                return this[CurrentIndex];
            }
        }

        public T Current => Count > 0 ? this[CurrentIndex] : default(T);
    }
}