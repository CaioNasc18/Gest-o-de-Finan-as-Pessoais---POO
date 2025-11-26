async function fetchCategories() {
    const res = await fetch('/categories');
    return await res.json();
}

async function fetchTransactions() {
    const res = await fetch('/transactions');
    return await res.json();
}

function renderTransactions(items, categories) {
    const tbody = document.querySelector('#tx-table tbody');
    tbody.innerHTML = '';
    items.forEach(function(t) {
        const cat = categories.find(function(c) { return c.id === t.categoryId; });
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${t.id}</td>
            <td>${t.description}</td>
            <td>${t.value.toFixed(2)}</td>
            <td>${new Date(t.date).toLocaleString()}</td>
            <td>${cat ? cat.name : '—'}</td>
            <td>${t.type}</td>
        `;
        tbody.appendChild(row);
    });
}

async function loadPage() {
    const categories = await fetchCategories();
    const txs = await fetchTransactions();

    const sel = document.getElementById('category-select');
    sel.innerHTML = '<option value="0">-- Sem categoria --</option>';
    categories.forEach(function(c) {
        const opt = document.createElement('option');
        opt.value = c.id;
        opt.textContent = c.name;
        sel.appendChild(opt);
    });

    renderTransactions(txs, categories);
}

document.getElementById('tx-form').addEventListener('submit', async function(e) {
    e.preventDefault();
    const desc = document.getElementById('desc').value || '';
    const value = parseFloat(document.getElementById('value').value);
    const categoryId = parseInt(document.getElementById('category-select').value, 10);
    const type = document.getElementById('type-select').value;

    const body = { description: desc, value: value, categoryId: categoryId, type: type };

    const res = await fetch('/transactions', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    });

    if (res.ok) {
        document.getElementById('tx-form').reset();
        await loadPage();
    } else {
        const err = await res.json();
        alert('Erro: ' + (err.error || 'pedido inválido'));
    }
});

window.addEventListener('load', loadPage);
