using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cephiro.Identity.Domain.Entities;

[Table(name: "user", Schema = "identity")]
public sealed class User
{
    [Column("id")] [Key]                              public Guid Id { get; set; }
    [Column("image_uri")] [MaxLength(400)]            public Uri? ImageUri { get; set; }
    [Column("description")] [MaxLength(800)]          public string? Description { get; set; }
    [Column("first_name")] [MaxLength(256)]           public required string FirstName { get; set; }
    [Column("middle_name")] [MaxLength(128)]          public string? MiddleName { get; set; }
    [Column("last_name")] [MaxLength(128)]            public required string LastName { get; set; }
    [Column("email")] [MaxLength(256)] [EmailAddress] public required string Email { get; set; }
    [Column("email_confirmed")]                       public bool EmailConfirmed { get; set; } = false;
    [Column("phone_number")] [MaxLength(16)] [Phone]  public string? PhoneNumber { get; set; }
    [Column("phone_number_confirmed")]                public bool PhoneNumberConfirmed { get; set; } = false;
    [Column("password_hash")]                         public byte[]? PasswordHash { get; set; }
    [Column("password_salt")]                         public byte[]? PasswordSalt { get; set; }
    [Column("has_credit_card")]                       public bool HasCreditCard { get; set; } = false;

    // the regularization stage increments by one when one of these conditions is fullfilled: 
    // email verified, phonenumber added, phonenumber verified, image added, description added, has creditcard
    // the regularization stage impacts the features a user can access and the visibility of his listings
    [Column("regularization_stage")] [Range(0, 6)]    public int RegularizationStage { get; set; } = 0;
    [Column(name: "status", TypeName = "smallint")]   public int ActivityStatus { get; set; }
}