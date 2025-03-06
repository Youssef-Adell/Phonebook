using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phonebook.Domain.Entities;

namespace Phonebook.Infrastructure.Persistence.EFConfig.Configs;

public class ContactConfig : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasIndex(c => c.Name)
            .HasMethod("GIN")
            .HasOperators("gin_trgm_ops"); // Creates a GIN index tailored for trigram-based text operations

        builder.HasIndex(c => c.PhoneNumber)
            .HasMethod("GIN")
            .HasOperators("gin_trgm_ops");

        builder.HasIndex(c => c.Email)
            .HasMethod("GIN")
            .HasOperators("gin_trgm_ops");


        /*
        When creating a GIN index with gin_trgm_ops operator:
            1) a trigram for each value in the indexed column is created:
                - Example: "hello" → {" h", " he", "hel", "ell", "llo", "lo "}

            2) a GIN Index is created:
                - wehere each trigram is used as a key and mapped to a list of rows containing it

        Then when I query using ILIKE '%searchTerm%':
            - The search term is split into trigrams
            - The GIN index finds rows matching those trigrams
        */
    }

}
