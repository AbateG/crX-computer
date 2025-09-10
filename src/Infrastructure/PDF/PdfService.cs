using System;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using CR_COMPUTER.Domain.Entities;
using CR_COMPUTER.Domain.Interfaces;

namespace CR_COMPUTER.Infrastructure.PDF
{
    /// <summary>
    /// PDF service implementation using iText7
    /// </summary>
    public class PdfService : IPdfService
    {
    public Task<byte[]> GenerateProjectReportAsync(Project project)
        {
            using var stream = new MemoryStream();
            using var writer = new PdfWriter(stream);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf);

            // Title
            document.Add(new Paragraph($"Project Report: {project.Name}")
                .SetFontSize(20)
                .SetMarginBottom(20));

            // Project details
            document.Add(new Paragraph($"Description: {project.Description}"));
            document.Add(new Paragraph($"Status: {project.Status}"));
            document.Add(new Paragraph($"Priority: {project.Priority}"));
            document.Add(new Paragraph($"Start Date: {project.StartDate:yyyy-MM-dd}"));
            document.Add(new Paragraph($"Due Date: {project.DueDate?.ToString("yyyy-MM-dd") ?? "Not set"}"));
            document.Add(new Paragraph($"Budget: ${project.Budget:N2}"));
            document.Add(new Paragraph($"Progress: {project.GetProgressPercentage():F1}%"));

            // Tasks summary
            document.Add(new Paragraph("\nTasks Summary:")
                .SetFontSize(16)
                .SetMarginTop(20)
                .SetMarginBottom(10));

            var table = new Table(4);
            table.AddHeaderCell("Task");
            table.AddHeaderCell("Status");
            table.AddHeaderCell("Assigned To");
            table.AddHeaderCell("Due Date");

            foreach (var task in project.Tasks)
            {
                table.AddCell(task.Title);
                table.AddCell(task.Status.ToString());
                table.AddCell(task.AssignedUser?.GetFullName() ?? "Unassigned");
                table.AddCell(task.DueDate?.ToString("yyyy-MM-dd") ?? "Not set");
            }

            document.Add(table);

            document.Close();
            return System.Threading.Tasks.Task.FromResult(stream.ToArray());
        }

    public Task<byte[]> GenerateInvoiceAsync(Project project, decimal amount, DateTime invoiceDate)
        {
            using var stream = new MemoryStream();
            using var writer = new PdfWriter(stream);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf);

            // Title
            document.Add(new Paragraph("INVOICE")
                .SetFontSize(24)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetMarginBottom(30));

            // Invoice details
            document.Add(new Paragraph($"Invoice Date: {invoiceDate:yyyy-MM-dd}"));
            document.Add(new Paragraph($"Project: {project.Name}"));
            document.Add(new Paragraph($"Client: {project.Client?.GetFullName() ?? "N/A"}"));
            document.Add(new Paragraph($"Amount: ${amount:N2}")
                .SetFontSize(18)
                .SetMarginTop(20));

            document.Close();
            return System.Threading.Tasks.Task.FromResult(stream.ToArray());
        }

    public Task<byte[]> GenerateWorkOrderAsync(CR_COMPUTER.Domain.Entities.Task task)
        {
            using var stream = new MemoryStream();
            using var writer = new PdfWriter(stream);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf);

            // Title
            document.Add(new Paragraph("WORK ORDER")
                .SetFontSize(24)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetMarginBottom(30));

            // Work order details
            document.Add(new Paragraph($"Task: {task.Title}"));
            document.Add(new Paragraph($"Description: {task.Description}"));
            document.Add(new Paragraph($"Project: {task.Project.Name}"));
            document.Add(new Paragraph($"Assigned To: {task.AssignedUser?.GetFullName() ?? "Unassigned"}"));
            document.Add(new Paragraph($"Priority: {task.Priority}"));
            document.Add(new Paragraph($"Due Date: {task.DueDate?.ToString("yyyy-MM-dd") ?? "Not set"}"));
            document.Add(new Paragraph($"Estimated Cost: ${task.EstimatedCost:N2}"));

            document.Close();
            return System.Threading.Tasks.Task.FromResult(stream.ToArray());
        }
    }
}
