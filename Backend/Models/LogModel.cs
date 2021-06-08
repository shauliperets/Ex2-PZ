using System;

namespace Backend.Models
{
    public class LogModel
    {
        uint id;
        string file;
        private string description;
        DateTime date;
        
        public uint ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string File
        {
            get
            {
                return this.file;
            }
            set
            {
                this.file = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
            }
        }
        
        
        public LogModel(string file, string description, DateTime date)
        {
            this.file = file;
            this.description = description;
            this.date = date;
        }
    }
}