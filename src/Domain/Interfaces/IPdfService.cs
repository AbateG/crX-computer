using System;
using System.Threading.Tasks;
using CR_COMPUTER.Domain.Entities;

namespace CR_COMPUTER.Domain.Interfaces
{
    /// <summary>
    /// PDF service interface for document generation
    /// </summary>
    public interface IPdfService
    {
    Task<byte[]> GenerateProjectReportAsync(Project project);
    Task<byte[]> GenerateInvoiceAsync(Project project, decimal amount, DateTime invoiceDate);
    Task<byte[]> GenerateWorkOrderAsync(CR_COMPUTER.Domain.Entities.Task task);
    }
}
