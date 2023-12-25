@section Scripts {
    <script>
        var data = @Html.Raw(Json.Serialize(Model));

        document.addEventListener("DOMContentLoaded", function () {
            const inputs = document.querySelectorAll('input[type="text"]');
            inputs.forEach(input => {
            input.addEventListener('input', function (e) {
                filterTable();
            });
            });
        });

        function filterTable() {
            const searchName = document.querySelector('input[name="searchName"]').value.toLowerCase();
        const searchSurname = document.querySelector('input[name="searchSurname"]').value.toLowerCase();
        const searchTelephone = document.querySelector('input[name="searchTelephone"]').value.toLowerCase();

        const tableRows = document.querySelectorAll('#tableBody tr');
            tableRows.forEach(row => {
                const rowData = row.textContent.toLowerCase() || row.innerText.toLowerCase();
        const nameMatch = rowData.includes(searchName);
        const surnameMatch = rowData.includes(searchSurname);
        const telephoneMatch = rowData.includes(searchTelephone);

        if (nameMatch && surnameMatch && telephoneMatch) {
            row.style.display = '';
                } else {
            row.style.display = 'none';
                }
            });
        }

        const itemsPerPage = 5;
        let currentPage = 1;

        function displayTable(currentPage) {
            const tableBody = document.getElementById('tableBody');
        tableBody.innerHTML = '';

        const startIndex = (currentPage - 1) * itemsPerPage;
        const endIndex = startIndex + itemsPerPage;
        const paginatedData = data.slice(startIndex, endIndex);
          
            paginatedData.forEach(item => {
                const row = document.createElement('tr');
        row.innerHTML = `
        <td>${item.name}</td>
        <td>${item.surname}</td>
        <td>${item.telephone}</td>
        <td>
            <a href="Directorys/Edit/${item.id}" class="btn btn-secondary">Düzenle</a>
            <a href="Directorys/Details/${item.id}" class="btn btn-info">Detay</a>
            <a href="Directorys/Delete/${item.id}" class="btn btn-danger">Sil</a>
        </td>
                        `;
                tableBody.appendChild(row);
            });
        }

        function displayPagination() {
            const pagination = document.getElementById('pagination');
            pagination.innerHTML = '';

            const pageCount = Math.ceil(data.length / itemsPerPage);
            for (let i = 1; i <= pageCount; i++) {
                const link = document.createElement('a');
                link.href = '#';
                link.textContent = i;
                link.addEventListener('click', function () {
                    currentPage = i;
                    displayTable(currentPage);
                });
                pagination.appendChild(link);
            }
        }

        displayTable(currentPage);
        displayPagination();
    </script>
}
