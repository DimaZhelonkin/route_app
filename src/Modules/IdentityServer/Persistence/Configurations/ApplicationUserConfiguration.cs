using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.IdentityServer.Domain.ApplicationUser.ValueObjects;
using Ark.SharedLib.Domain.ValueObjects.Emails;
using Ark.SharedLib.Domain.ValueObjects.PhoneNumbers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ark.IdentityServer.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    #region IEntityTypeConfiguration<ApplicationUser> Members

    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(user => user.Id);

        builder.OwnsOne(user => user.FirstName, firstNameBuilder =>
        {
            firstNameBuilder.WithOwner();

            firstNameBuilder.Property(firstName => firstName.Value)
                            .HasColumnName(nameof(ApplicationUser.FirstName))
                            .HasMaxLength(FirstName.MaxLength)
                            .IsRequired();
        });

        builder.OwnsOne(user => user.LastName, lastNameBuilder =>
        {
            lastNameBuilder.WithOwner();

            lastNameBuilder.Property(lastName => lastName.Value)
                           .HasColumnName(nameof(ApplicationUser.LastName))
                           .HasMaxLength(LastName.MaxLength)
                           .IsRequired();
        });

        builder.OwnsOne(user => user.Email, emailBuilder =>
        {
            emailBuilder.WithOwner();

            emailBuilder.Property(email => email.Value)
                        .HasColumnName(nameof(ApplicationUser.Email))
                        .HasMaxLength(Email.MaxLength)
                        .IsRequired();
        });

        builder.OwnsOne(user => user.PhoneNumber, emailBuilder =>
        {
            emailBuilder.WithOwner();

            emailBuilder.Property(phoneNumber => phoneNumber.Value)
                        .HasColumnName(nameof(ApplicationUser.PhoneNumber))
                        .HasMaxLength(PhoneNumber.MaxLength)
                        .IsRequired();
        });

        builder.OwnsOne(user => user.Password, emailBuilder =>
        {
            emailBuilder.WithOwner();

            emailBuilder.Property(password => password.Value)
                        .HasColumnName("HashedPassword")
                        .HasMaxLength(Password.MaxLength)
                        .IsRequired();
        });

        // TODO get it from Password VO
        // builder.Property<string>("_passwordHash")
        //        .HasField("_passwordHash")
        //        .HasColumnName("PasswordHash")
        //        .IsRequired();

        builder.Property(user => user.CreatedOnUtc).IsRequired();
        builder.Property(user => user.ModifiedOnUtc);
        builder.Property(user => user.DeletedOnUtc);
        builder.Property(user => user.IsDeleted)
               .HasDefaultValue(false);

        // It hidden in repository now, because we can hard delete entity if we want to.
        // builder.HasQueryFilter(user => !user.IsDeleted);

        builder.Ignore(user => user.FullName);
    }

    #endregion
}