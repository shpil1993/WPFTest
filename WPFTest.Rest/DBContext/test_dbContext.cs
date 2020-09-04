using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WPFTest.Rest.Models;

namespace WPFTest.Rest.DBContext
{
    public partial class test_dbContext : DbContext
    {
        public test_dbContext()
        {
        }

        public test_dbContext(DbContextOptions<test_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Greeting> Greeting { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonContact> PersonContact { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;user=root;password=test1234;database=test_db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PRIMARY");

                entity.ToTable("country");

                entity.HasIndex(e => e.Txt1)
                    .HasName("UK_TXT1")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasColumnName("CODE")
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Active)
                    .HasColumnName("ACTIVE")
                    .HasDefaultValueSql("'1'")
                    .HasComment("0: inactive, 1: active");

                entity.Property(e => e.AddrFormatId)
                    .HasColumnName("ADDR_FORMAT_ID")
                    .HasDefaultValueSql("'1'")
                    .HasComment("1: CountryCode / Zip / City, 2: Zip / City / Country, 3: City / Zip / Country");

                entity.Property(e => e.IntDialCode)
                    .HasColumnName("INT_DIAL_CODE")
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasComment("international dial code");

                entity.Property(e => e.IsVatIncluded)
                    .HasColumnName("IS_VAT_INCLUDED")
                    .HasDefaultValueSql("'1'")
                    .HasComment("0: no, 1: yes - for future use");

                entity.Property(e => e.Txt1)
                    .IsRequired()
                    .HasColumnName("TXT1")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Txt2)
                    .HasColumnName("TXT2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Txt3)
                    .HasColumnName("TXT3")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Txt4)
                    .HasColumnName("TXT4")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Greeting>(entity =>
            {
                entity.ToTable("greeting");

                entity.HasIndex(e => e.Txt1)
                    .HasName("UK_TXT1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .HasColumnName("ACTIVE")
                    .HasDefaultValueSql("'1'")
                    .HasComment("0: inactive, 1: active");

                entity.Property(e => e.Txt1)
                    .IsRequired()
                    .HasColumnName("TXT1")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Txt2)
                    .HasColumnName("TXT2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Txt3)
                    .HasColumnName("TXT3")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Txt4)
                    .HasColumnName("TXT4")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TxtLetter1)
                    .IsRequired()
                    .HasColumnName("TXT_LETTER1")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TxtLetter2)
                    .HasColumnName("TXT_LETTER2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TxtLetter3)
                    .HasColumnName("TXT_LETTER3")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TxtLetter4)
                    .HasColumnName("TXT_LETTER4")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.HasIndex(e => e.City)
                    .HasName("IDX_CITY");

                entity.HasIndex(e => e.CountryCode)
                    .HasName("FK_person_country");

                entity.HasIndex(e => e.Cpny)
                    .HasName("IDX_CPNY");

                entity.HasIndex(e => e.GreetingId)
                    .HasName("FK_person_greeting");

                entity.HasIndex(e => e.Lname)
                    .HasName("IDX_NAME");

                entity.HasIndex(e => e.Zip)
                    .HasName("IDX_ZIP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("CITY")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnName("COUNTRY_CODE")
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Cpny)
                    .HasColumnName("CPNY")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("NAME or CPNY are mandatory; must be ensured by application");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("DATE_OF_BIRTH")
                    .HasColumnType("date");

                entity.Property(e => e.FirstContact)
                    .HasColumnName("FIRST_CONTACT")
                    .HasColumnType("date")
                    .HasComment("Record creation / Set on insert");

                entity.Property(e => e.Fname)
                    .HasColumnName("FNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GenderId)
                    .HasColumnName("GENDER_ID")
                    .HasComment("1: without, 2: male, 3: female, 4: other");

                entity.Property(e => e.GreetingId).HasColumnName("GREETING_ID");

                entity.Property(e => e.Lname)
                    .HasColumnName("LNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("NAME or CPNY are mandatory; must be ensured by application");

                entity.Property(e => e.Notes).HasColumnName("NOTES");

                entity.Property(e => e.Street)
                    .HasColumnName("STREET")
                    .HasComment("multiline");

                entity.Property(e => e.Title)
                    .HasColumnName("TITLE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasColumnName("ZIP")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_person_country");

                entity.HasOne(d => d.Greeting)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.GreetingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_person_greeting");
            });

            modelBuilder.Entity<PersonContact>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.PersonContactId })
                    .HasName("PRIMARY");

                entity.ToTable("person_contact");

                entity.Property(e => e.PersonId).HasColumnName("PERSON_ID");

                entity.Property(e => e.PersonContactId)
                    .HasColumnName("PERSON_CONTACT_ID")
                    .HasComment("part of primary key");

                entity.Property(e => e.Active)
                    .HasColumnName("ACTIVE")
                    .HasDefaultValueSql("'1'")
                    .HasComment("0: inactive, 1: active");

                entity.Property(e => e.ContactTypeId).HasColumnName("CONTACT_TYPE_ID");

                entity.Property(e => e.Notes).HasColumnName("NOTES");

                entity.Property(e => e.Txt)
                    .HasColumnName("TXT")
                    .HasComment("e.g. phone number");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonContact)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_person_contact_person");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        public override int SaveChanges()
        {
            BeforeSaveChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            BeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void BeforeSaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                SavePerson(entry, entry.Entity as Person);
                SavePersonContact(entry, entry.Entity as PersonContact);
            }
        }

        private void SavePerson(EntityEntry entry, Person person)
        {
            if (person == null)
            {
                return;
            }
            if (entry.State == EntityState.Added)
            {
                person.FirstContact = DateTime.Now;
            }
        }

        private void SavePersonContact(EntityEntry entry, PersonContact personContact)
        {
            if (personContact == null)
            {
                return;
            }
            if (entry.State == EntityState.Added)
            {
                var lastContactId = PersonContact.Where(e => e.PersonId == personContact.PersonId).ToList().LastOrDefault();
                if (lastContactId != null)
                {
                    personContact.PersonContactId = lastContactId.PersonContactId + 1;
                    return;
                }
                personContact.PersonContactId = 1;
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
