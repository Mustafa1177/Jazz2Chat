﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jazz2Chat.JJ2.DataClasses
{
    public class GameServer
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public ServerLocation Location { get; set; }
        public bool IsPrivate { get; set; }
        public byte GameType { get; set; }
        public char[] Version { get; set; } = {'2','4',' ',' ' };
        public long Uptime { get; set; } = -1;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public byte PlayerCount { get; set; }
        public byte PlayerLimit { get; set; }
       

        public GameServer()
        {
        }

        public static GameServer Parse(string value)
        {
            return GameServer.Parse(value, DateTime.Now);
        }

            public static GameServer Parse(string value, DateTime t)
        {   
            try
            {
                GameServer res = new GameServer();
                int i1, i2;
                i1 = value.IndexOf(':');
                res.IP = value.Substring(0, i1++);

                i2 = value.IndexOf(' ', i1);
                res.Port = int.Parse(value.Substring(i1, i2 - i1));

                i2++;
                i1 = value.IndexOf(' ', i2);
                string loc = value.Substring(i2, i1 - i2);
                switch (loc)
                {
                    case "local":
                        res.Location = ServerLocation.LOCAL;
                        break;
                    case "mirror":
                        res.Location = ServerLocation.MIRROR;
                        break;
                    case "manual":
                        res.Location = ServerLocation.MANUAL;
                        break;
                    case "clone":
                        res.Location = ServerLocation.CLONE;
                        break;
                }

                i1++;
                i2 = value.IndexOf(' ', i1);
                string servType = value.Substring(i1, i2 - i1);
                res.IsPrivate = (servType == "private" ? true : false);

                i2++;
                i1 = value.IndexOf(' ', i2);
                string mode = value.Substring(i2, i1 - i2);
                switch (mode)
                {
                    case "battle":
                        res.GameType = 2;
                        break;
                    case "capture":
                    case "ctf":
                        res.GameType = 5;
                        break;
                    case "treasure":
                        res.GameType = 4;
                        break;
                    case "coop":
                        res.GameType = 1;
                        break;
                    case "race":
                        res.GameType = 3;
                        break;
                    case "teambattle":
                        res.GameType = 2;
                        break;
                    default:
                        res.GameType = 6;
                        break;
                }

                i1 += 3;
                for (int i = 0; i < 4; ++i)
                    res.Version[i] = value[i1 + i];

                i1 += 5;
                i2 = value.IndexOf(' ', i1);
                res.Uptime = long.Parse(value.Substring(i1, i2 - i1));

                // [1/32]
                i2 += 2;
                i1 = value.IndexOf('/', i2);
                res.PlayerCount = byte.Parse(value.Substring(i2, i1 - i2));

                i1++;
                i2 = value.IndexOf(']', i1);
                res.PlayerLimit = byte.Parse(value.Substring(i1, i2 - i1));

                i2 += 2;
                res.Name = value.Substring(i2);
                return res;
            }
            catch(Exception ex)
            {
                return null;
            }
           
        }
    }

    public enum ServerLocation
    {
        LOCAL,
        MIRROR,
        MANUAL,
        CLONE
    }
}
