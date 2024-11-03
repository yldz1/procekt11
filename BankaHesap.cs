﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2Video
{
    abstract class BankaHesap
    {
        public long HesapNo { get; set; }
        public int SubeKodu { get; set; }
        public string IBAN { get; set; }
        public decimal Bakiye { get; set; }

        public virtual string ParaCek(decimal tutar)
        { 
            Bakiye -= tutar;
            return "Hesabınızdan " + tutar + " TL para çektiniz. Güncel bakiyeniz: " + Bakiye;
        }
        public virtual string ParaYatir(decimal tutar)
        {
            Bakiye += tutar;
            return "Hesabınızdan " + tutar + " TL para yatırdınız. Güncel bakiyeniz: " + Bakiye;
        }

    }

    class VadesizHesap:BankaHesap
    {
        public override string ParaCek(decimal tutar)
        {
            if (Bakiye<tutar)
            {
                return "Yetersiz Bakiye";
            }

            if (tutar%5==0)
            {
                return base.ParaCek(tutar);
            }
            else
            {
                return "5 ve 5 in katalrını çekebilirsiniz";
            }
        }

    }

    class VadeliHesap:BankaHesap
    {
        public DateTime VadeBaslangicTarihi { get; set; }
        public int VadeGunSayisi { get; set; }

        public DateTime VadeSonuTarihi
        {
            get { return VadeBaslangicTarihi.AddDays(VadeGunSayisi); }
        }
        public override string ParaCek(decimal tutar)
        {
            if (DateTime.Today.Date != VadeSonuTarihi.Date)
                return "Vade sonu tarihini bekleyiniz";
            else if (Bakiye < tutar)
                return "Yetersiz Bakiye";
            else if (tutar%5!=0)
            {
                return "5 ve 5 in katlarini cekebilirsin";
            }
            else
            {
                return base.ParaCek(tutar);
            }
           
        }
        public override string ParaYatir(decimal tutar)
        {
            if (DateTime.Today.Date==VadeSonuTarihi.Date)
            {
                return base.ParaYatir(tutar);
            }
            else
            {
                return "Vade sonu tarihini bekleyiniz";
            }

        }
    }
}
