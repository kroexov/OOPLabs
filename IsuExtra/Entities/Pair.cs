using System;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Pair
    {
        private int _minNumber = 1;
        private int _maxNumber = 8;
        private OGNPStream _stream;
        private int _number;
        private DayOfWeek _dayOfWeek;
        private string _teacher;
        private string _auditory;

        public Pair(OGNPStream stream, int number, DayOfWeek day, string teacher, string auditory)
        {
            if (number >= _minNumber && number <= _maxNumber)
            {
                _number = number;
                _dayOfWeek = day;
            }
            else
            {
                throw new IsuExtraException("Pair is not valid!");
            }

            _stream = stream;
            _teacher = teacher;
            _auditory = auditory;
        }

        public Pair(int number, DayOfWeek day, string teacher, string auditory)
        {
            if (number >= _minNumber && number <= _maxNumber)
            {
                _number = number;
                _dayOfWeek = day;
            }
            else
            {
                throw new IsuExtraException("Pair is not valid!");
            }

            _stream = new OGNPStream();
            _teacher = teacher;
            _auditory = auditory;
        }

        public OGNPStream Stream
        {
            get
            {
                return _stream;
            }
        }

        public int Number
        {
            get
            {
                return _number;
            }

            set
            {
                _number = value;
            }
        }

        public DayOfWeek OfWeekDay
        {
            get
            {
                return _dayOfWeek;
            }

            set
            {
                _dayOfWeek = value;
            }
        }

        public string Teacher
        {
            get
            {
                return _teacher;
            }

            set
            {
                _teacher = value;
            }
        }

        public string Auditory
        {
            get
            {
                return _auditory;
            }

            set
            {
                _auditory = value;
            }
        }
    }
}