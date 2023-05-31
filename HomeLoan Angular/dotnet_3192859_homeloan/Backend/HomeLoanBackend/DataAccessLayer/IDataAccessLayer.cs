using DataAccessLayer.ReadAndWriteFactory;

namespace DataAccessLayer
{
    public interface IDataAccessLayer
    {
        public IReadFactory Read();
        public IWriteFactory Write();
    }
}
