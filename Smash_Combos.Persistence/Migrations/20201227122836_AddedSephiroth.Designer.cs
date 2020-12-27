﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Smash_Combos.Persistence;

namespace Smash_Combos.Persistence.Migrations
{
    [DbContext(typeof(PostgreSqlDatabaseContext))]
    [Migration("20201227122836_AddedSephiroth")]
    partial class AddedSephiroth
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Smash_Combos.Domain.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("ReleaseOrder")
                        .HasColumnType("numeric");

                    b.Property<string>("VariableName")
                        .HasColumnType("text");

                    b.Property<int>("YPosition")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.Combo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<string>("ComboInput")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Damage")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NetVote")
                        .HasColumnType("integer");

                    b.Property<string>("Notes")
                        .HasColumnType("character varying(512)")
                        .HasMaxLength(512);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<bool>("TrueCombo")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("VideoEndTime")
                        .HasColumnType("integer");

                    b.Property<string>("VideoId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VideoStartTime")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("UserId");

                    b.ToTable("Combos");
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.ComboVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("ComboId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsUpvote")
                        .HasColumnType("boolean");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComboId");

                    b.HasIndex("UserId");

                    b.ToTable("ComboVotes");
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ComboId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("NetVote")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComboId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.CommentVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CommentId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsUpvote")
                        .HasColumnType("boolean");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("UserId");

                    b.ToTable("CommentVotes");
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.Infraction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("BanDuration")
                        .HasColumnType("integer");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateInfracted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DismissDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ModeratorId")
                        .HasColumnType("integer");

                    b.Property<int?>("Points")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ModeratorId");

                    b.HasIndex("UserId");

                    b.ToTable("Infractions");
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ComboId")
                        .HasColumnType("integer");

                    b.Property<int?>("CommentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateReported")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Dismiss")
                        .HasColumnType("boolean");

                    b.Property<int?>("ReporterId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComboId");

                    b.HasIndex("CommentId");

                    b.HasIndex("ReporterId");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("text");

                    b.Property<bool>("PasswordMeetsCriteria")
                        .HasColumnType("boolean");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.Combo", b =>
                {
                    b.HasOne("Smash_Combos.Domain.Models.Character", "Character")
                        .WithMany("Combos")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Smash_Combos.Domain.Models.User", "User")
                        .WithMany("Combos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.ComboVote", b =>
                {
                    b.HasOne("Smash_Combos.Domain.Models.Combo", "Combo")
                        .WithMany()
                        .HasForeignKey("ComboId");

                    b.HasOne("Smash_Combos.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.Comment", b =>
                {
                    b.HasOne("Smash_Combos.Domain.Models.Combo", "Combo")
                        .WithMany("Comments")
                        .HasForeignKey("ComboId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Smash_Combos.Domain.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.CommentVote", b =>
                {
                    b.HasOne("Smash_Combos.Domain.Models.Comment", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId");

                    b.HasOne("Smash_Combos.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.Infraction", b =>
                {
                    b.HasOne("Smash_Combos.Domain.Models.User", "Moderator")
                        .WithMany()
                        .HasForeignKey("ModeratorId");

                    b.HasOne("Smash_Combos.Domain.Models.User", "User")
                        .WithMany("Infractions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Smash_Combos.Domain.Models.Report", b =>
                {
                    b.HasOne("Smash_Combos.Domain.Models.Combo", "Combo")
                        .WithMany("Reports")
                        .HasForeignKey("ComboId");

                    b.HasOne("Smash_Combos.Domain.Models.Comment", "Comment")
                        .WithMany("Reports")
                        .HasForeignKey("CommentId");

                    b.HasOne("Smash_Combos.Domain.Models.User", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId");

                    b.HasOne("Smash_Combos.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
