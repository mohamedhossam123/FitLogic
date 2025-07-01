using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models;

public partial class FitLogicContext : DbContext
{
    public FitLogicContext()
    {
    }

    public FitLogicContext(DbContextOptions<FitLogicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseMuscleTarget> ExerciseMuscleTargets { get; set; }

    public virtual DbSet<ExerciseType> ExerciseTypes { get; set; }

    public virtual DbSet<MuscleGroup> MuscleGroups { get; set; }

    public virtual DbSet<SkillLevel> SkillLevels { get; set; }

    public virtual DbSet<SubMuscle> SubMuscles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-FM1OTR0;Database=FitLogic;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__A074AD0F00C47894");

            entity.Property(e => e.ExerciseId).HasColumnName("ExerciseID");
            entity.Property(e => e.ExerciseName)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.ExerciseTypeId).HasColumnName("ExerciseTypeID");
            entity.Property(e => e.SkillLevelId).HasColumnName("SkillLevelID");

            entity.HasOne(d => d.ExerciseType).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.ExerciseTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exercises__Exerc__412EB0B6");

            entity.HasOne(d => d.SkillLevel).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.SkillLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exercises__Skill__403A8C7D");
        });

        modelBuilder.Entity<ExerciseMuscleTarget>(entity =>
        {
            entity.HasKey(e => new { e.ExerciseId, e.SubMuscleId }).HasName("PK__Exercise__9B3B694E01DA48B7");

            entity.ToTable("ExerciseMuscleTarget");

            entity.Property(e => e.ExerciseId).HasColumnName("ExerciseID");
            entity.Property(e => e.SubMuscleId).HasColumnName("SubMuscleID");
            entity.Property(e => e.MuscleGroupId).HasColumnName("MuscleGroupID");
            entity.Property(e => e.Percentage).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseMuscleTargets)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExerciseM__Exerc__440B1D61");

            entity.HasOne(d => d.MuscleGroup).WithMany(p => p.ExerciseMuscleTargets)
                .HasForeignKey(d => d.MuscleGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExerciseM__Muscl__44FF419A");

            entity.HasOne(d => d.SubMuscle).WithMany(p => p.ExerciseMuscleTargets)
                .HasForeignKey(d => d.SubMuscleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExerciseM__SubMu__45F365D3");
        });

        modelBuilder.Entity<ExerciseType>(entity =>
        {
            entity.HasKey(e => e.ExerciseTypeId).HasName("PK__Exercise__6CBFB0D7BA431728");

            entity.Property(e => e.ExerciseTypeId).HasColumnName("ExerciseTypeID");
            entity.Property(e => e.ExerciseTypeName)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<MuscleGroup>(entity =>
        {
            entity.HasKey(e => e.MuscleGroupId).HasName("PK__MuscleGr__097AE806D5FA3272");

            entity.Property(e => e.MuscleGroupId).HasColumnName("MuscleGroupID");
            entity.Property(e => e.MuscleGroupName)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<SkillLevel>(entity =>
        {
            entity.HasKey(e => e.SkillLevelId).HasName("PK__SkillLev__927B2DA7BC1D5507");

            entity.Property(e => e.SkillLevelId).HasColumnName("SkillLevelID");
            entity.Property(e => e.LevelName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<SubMuscle>(entity =>
        {
            entity.HasKey(e => e.SubMuscleId).HasName("PK__SubMuscl__B4FC4412D98268CB");

            entity.Property(e => e.SubMuscleId).HasColumnName("SubMuscleID");
            entity.Property(e => e.MuscleGroupId).HasColumnName("MuscleGroupID");
            entity.Property(e => e.SubMuscleName)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.MuscleGroup).WithMany(p => p.SubMuscles)
                .HasForeignKey(d => d.MuscleGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubMuscle__Muscl__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
