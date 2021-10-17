using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Pair
    {
        private OGNPStream _stream;
        private int _number;
        private int _day;
        private string _teacher;
        private int _auditory;

        public Pair(OGNPStream stream, int number, int day, string teacher, int auditory)
        {
            if (number > 0 && number < 8 && day > 0 && day < 7)
            {
                _number = number;
                _day = day;
            }
            else
            {
                throw new IsuExtraException("Pair is not valid!");
            }

            _stream = stream;
            _teacher = teacher;
            _auditory = auditory;
        }

        public Pair(int number, int day, string teacher, int auditory)
        {
            if (number > 0 && number < 8 && day > 0 && day < 7)
            {
                _number = number;
                _day = day;
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

        public int Day
        {
            get
            {
                return _day;
            }

            set
            {
                _day = value;
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

        public int Auditory
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