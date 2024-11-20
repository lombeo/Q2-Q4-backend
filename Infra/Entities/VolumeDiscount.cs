using Api_Project_Prn.Infra.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Api_Project_Prn.Infra.Entities
{
    public class VolumeDiscount
    {
        public int Id { get; set; }
        public string Campaign { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<Rule> DiscountRule { get; set; }
    }

    public class Rule
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string? Label { get; set; }
        public int Quantity { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal? Amount { get; set; }
    }

    public static class VolumeDiscountConfiguration
    {
        public static void Config(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VolumeDiscount>(entity =>
            {
                entity.ToTable("volume_discounts");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Campaign)
                    .HasColumnName("campaign")
                    .IsRequired();

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.DiscountRule)
                    .HasColumnName("discount_rules")
                    .HasColumnType("jsonb")
                    .HasConversion(
                        v => JsonConvert.SerializeObject(v),
                        v => JsonConvert.DeserializeObject<List<Rule>>(v) 
                    );
            });
        }
    }

}
