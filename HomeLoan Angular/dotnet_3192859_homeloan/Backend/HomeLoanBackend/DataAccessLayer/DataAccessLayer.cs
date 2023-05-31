using DataAccessLayer.Data;
using DataAccessLayer.ReadAndWriteFactory;

namespace DataAccessLayer
{
    public class DataAccessLayer:IDataAccessLayer
    {
        private readonly IReadFactory _readFactory;
        private readonly IWriteFactory _writeFactory;
        private readonly AppDbContext _appDbContext;
        public DataAccessLayer()
        {
            _appDbContext= new AppDbContext();
            _readFactory = new ReadFactory(_appDbContext);
            _writeFactory= new WriteFactory(_appDbContext);
        }
        public IReadFactory Read()
        {
            return _readFactory;
        }
        public IWriteFactory Write()
        {
            return _writeFactory;
        }
    }
}
