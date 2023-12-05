﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateQuizBowlOnBlobUpdate.Models
{
        public class Question
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public string Answer { get; set; }

            public int Points { get; set; }
            public string Category { get; set; }
        }
    }