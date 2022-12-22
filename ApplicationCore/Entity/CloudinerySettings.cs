namespace ApplicationCore.Entity
{
    public class CloudinarySettings
    {
        public string CloudName { get; set; }
        public string APIKey { get; set; }
        public string APISecret { get; set; }

        public virtual CloudinarySettings CreateRecords()
        {
            return null;
        }
    }

}
