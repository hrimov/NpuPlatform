import { Link } from "react-router-dom";
import { Button, Space, Dropdown } from "antd";
import { User, LogOut } from "lucide-react";
import { useAuth } from "../contexts/AuthContext";
import { logout } from "../services/auth";

export default function Navbar() {
  const { user, isAuthenticated } = useAuth();

  const userMenuItems = [
    {
      key: "profile",
      label: <Link to="/profile">Profile</Link>,
      icon: <User size={16} />,
    },
    {
      key: "logout",
      label: "Logout",
      icon: <LogOut size={16} />,
      onClick: logout,
    },
  ];

  return (
    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4">
      <div className="flex justify-between items-center">
        <Link to="/" className="text-2xl font-bold">
          NPU Gallery
        </Link>

        <Space>
          {isAuthenticated ? (
            <>
              <Dropdown menu={{ items: userMenuItems }} placement="bottomRight">
                <Button type="text">{user?.username}</Button>
              </Dropdown>
            </>
          ) : (
            <Space>
              <Link to="/login">
                <Button type="primary">Sign In</Button>
              </Link>
              <Link to="/register">
                <Button>Register</Button>
              </Link>
            </Space>
          )}
        </Space>
      </div>
    </div>
  );
}
