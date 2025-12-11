// script.js — consome a API em português (camelCase JSON)

async function fetchCategorias() {
    const response = await fetch("/categorias");
    return await response.json();
}

async function fetchTransacoes() {
    const response = await fetch("/transacoes");
    return await response.json();
}

function formatCurrency(v) {
    return Number(v).toFixed(2);
}

function renderTransacoes(items, categorias) {
    const tbody = document.querySelector('#tx-table tbody');
    tbody.innerHTML = '';
    items.forEach(function (t) {
        const cat = categorias.find(function (c) { return c.id === t.categoriaId; });
        const row = document.createElement('tr');

        // botões editar / apagar
        const actions = `
            <button data-id="${t.id}" class="edit-btn">Editar</button>
            <button data-id="${t.id}" class="delete-btn">Apagar</button>
        `;

        const tipo = document.getElementById('type-select').value === "Receita" ? 0 : 1;

        row.innerHTML = `
            <td>${t.id}</td>
            <td>${t.descricao}</td>
            <td>${formatCurrency(t.valor)}</td>
            <td>${new Date(t.data).toLocaleString()}</td>
            <td>${cat ? cat.nome : '—'}</td>
            <td>${t.tipo}</td>
            <td>${actions}</td>
        `;
        tbody.appendChild(row);
    });

    // ligar eventos dos botões
    document.querySelectorAll('.delete-btn').forEach(function (btn) {
        btn.addEventListener('click', async function () {
            const id = btn.getAttribute('data-id');
            if (!confirm('Eliminar transação #' + id + '?')) return;
            const res = await fetch('/transacoes/' + id, { method: 'DELETE' });
            if (res.ok) {
                await loadPage();
            } else {
                alert('Erro ao apagar');
            }
        });
    });

    document.querySelectorAll('.edit-btn').forEach(function (btn) {
        btn.addEventListener('click', async function () {
            const id = btn.getAttribute('data-id');
            const descricao = prompt('Nova descrição:');
            const valorStr = prompt('Novo valor:');
            const valor = parseFloat(valorStr);
            if (isNaN(valor) || valor <= 0) { alert('Valor inválido'); return; }
            // podes escolher categoria/tipo manualmente se quiseres; aqui fazemos um PUT simples
            const payload = { descricao: descricao, valor: valor };
            const res = await fetch('/transacoes/' + id, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });
            if (res.ok) {
                await loadPage();
            } else {
                const err = await res.json();
                alert('Erro: ' + (err.error || 'não foi possível editar'));
            }
        });
    });
}

async function loadPage() {
    try {
        const categorias = await fetchCategorias();
        const transacoes = await fetchTransacoes();

        // popular select
        const sel = document.getElementById('category-select');
        sel.innerHTML = '<option value="0">-- Sem categoria --</option>';
        categorias.forEach(function (c) {
            const opt = document.createElement('option');
            opt.value = c.id;
            opt.textContent = c.nome;
            sel.appendChild(opt);
        });

        renderTransacoes(transacoes, categorias);
    } catch (err) {
        console.error(err);
        alert('Erro a carregar dados do servidor.');
    }
}

document.getElementById('tx-form').addEventListener('submit', async function (e) {
    e.preventDefault();
    const desc = document.getElementById('desc').value || '';
    const value = parseFloat(document.getElementById('value').value);
    const categoriaId = parseInt(document.getElementById('category-select').value, 10);
    const tipoValue = document.getElementById('type-select').value;
    const tipo = tipoValue === "Receita" ? 0 : 1;

    const body = {
        descricao: desc,
        valor: value,
        categoriaId: categoriaId,
        tipo: tipo
    };


    if (isNaN(value) || value <= 0) {
        alert('Insira um valor válido.');
        return;
    }

    const res = await fetch("/transacoes", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(body)
    });

    if (res.ok) {
        document.getElementById("tx-form").reset();
        await loadPage();
    } else {
        const err = await res.json();
        alert("Erro: " + (err.error || "Pedido inválido"));
    }
});

// função de login (exemplo)
async function login(email, senha) {
    const res = await fetch('/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: email, senha: senha })
    });
    if (res.ok) {
        const user = await res.json();
        alert('Login OK: ' + user.nome);
        return user;
    } else {
        alert('Credenciais inválidas');
        return null;
    }
}

// função de relatório (exemplo)
async function gerarRelatorio(inicio, fim) {
    const res = await fetch(`/relatorios?inicio=${encodeURIComponent(inicio)}&fim=${encodeURIComponent(fim)}`);
    if (!res.ok) {
        alert('Erro ao gerar relatório');
        return;
    }
    const data = await res.json();
    console.log('Relatório', data);
    alert(`Receitas: ${data.totalReceitas}\nDespesas: ${data.totalDespesas}`);
}

window.addEventListener('load', loadPage);
