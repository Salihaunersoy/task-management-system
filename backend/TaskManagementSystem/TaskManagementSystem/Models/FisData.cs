namespace TaskManagementSystem.Models
{
	public class FisData
	{
		public int		FisId	  { get; set; }
		public string   FisNo     { get; set; }
		public DateTime FisTarih  { get; set; }
		public string   HesapKodu { get; set; }
		public string   HesapAdi  { get; set; }
		public string   Aciklama  { get; set; }
		public string   FaturaNo  { get; set; }
		public decimal  Borc      { get; set; }
		public decimal  Alacak    { get; set; }
	}
}
