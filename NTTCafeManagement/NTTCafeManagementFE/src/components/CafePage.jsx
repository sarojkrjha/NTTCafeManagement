import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { fetchCafes } from '../redux/cafeSlice';
import { Button, Table, Input, Select } from 'antd';
import { useNavigate } from 'react-router-dom';

const { Option } = Select;

const CafePage = () => {
    const { cafes, loading } = useSelector((state) => state.cafes);
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const [filterLocation, setFilterLocation] = useState('');
    const [filteredCafes, setFilteredCafes] = useState([]);

    // Fetch cafes on component mount
    useEffect(() => {
        dispatch(fetchCafes());
    }, [dispatch]);

    // Filter cafes by location
    useEffect(() => {
        if (filterLocation) {
            setFilteredCafes(cafes.filter((cafe) => cafe.location.toLowerCase().includes(filterLocation.toLowerCase())));
        } else {
            setFilteredCafes(cafes);
        }
    }, [cafes, filterLocation]);

    const handleDelete = (id) => {
        // Call API to delete the café
        console.log(`Delete café with ID: ${id}`);
    };

    const columns = [
        {
            title: 'Logo',
            dataIndex: 'logo',
            key: 'logo',
            render: (logo) => (
                <img src={logo} alt="Logo" style={{ width: '50px', height: '50px', objectFit: 'cover' }} />
            ),
        },
        { title: 'Name', dataIndex: 'name', key: 'name' },
        { title: 'Description', dataIndex: 'description', key: 'description' },
        { title: 'Location', dataIndex: 'location', key: 'location' },
        {
            title: 'Employees',
            dataIndex: 'employees',
            key: 'employees',
            render: (employees, record) => (
                <Button
                    type="link"
                    onClick={() => navigate(`/employees?cafeId=${record.employees}`)}
                >
                    {employees.length}
                </Button>
            ),
        },
        {
            title: 'Actions',
            key: 'actions',
            render: (_, record) => (
                <>
                    <Button
                        onClick={() => navigate(`/cafes/edit/${record.id}`)}
                        style={{ marginRight: 8 }}
                    >
                        Edit
                    </Button>
                    <Button type="danger" onClick={() => handleDelete(record.id)}>
                        Delete
                    </Button>
                </>
            ),
        },
    ];

    return (
        <div style={{ padding:'2%', width: '75%' }}>
            <Table
                dataSource={filteredCafes}
                columns={columns}
                rowKey="id"
                loading={loading}
            />
            <div style={{ marginBottom: 16, display: 'flex', justifyContent: 'space-between' }}>
                <Input
                    placeholder="Filter by location"
                    value={filterLocation}
                    onChange={(e) => setFilterLocation(e.target.value)}
                    style={{ width: 200 }}
                />
                <Button
                    type="primary"
                    onClick={() => navigate('/cafes/add')}
                >
                    Add New Café
                </Button>
            </div>

            
        </div>
    );
};

export default CafePage;
