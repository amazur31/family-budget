// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "Not needed for Tests", Scope = "member", Target = "~F:Tivix.FamilyBudget.Server.Core.Tests.Mocks.UserProviderMock")]
