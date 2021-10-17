using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class OGNPCourse
    {
        private string _name;
        private Megafacultet _megafacultet;
        private List<OGNPStream> _streams = new List<OGNPStream>();
        private List<Student> _students = new List<Student>();

        public OGNPCourse(string name, Megafacultet megafacultet)
        {
            _name = name;
            _megafacultet = megafacultet;
        }

        public OGNPCourse()
        {
            _megafacultet = new Megafacultet();
        }

        public void AddStream(OGNPStream stream)
        {
            _streams.Add(stream);
        }

        public char GetMegafacultetAcronym()
        {
            return _megafacultet.GetAcronym();
        }
    }
}