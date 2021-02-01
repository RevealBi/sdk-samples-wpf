namespace LoadDashboardsFromFolder.Business
{
    public class DashboardItem
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
