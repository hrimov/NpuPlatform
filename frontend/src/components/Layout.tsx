import { Outlet } from "react-router-dom";
import { Layout as AntLayout } from "antd";
import { Toaster } from "react-hot-toast";
import Navbar from "./Navbar";

const { Header, Content } = AntLayout;

export default function Layout() {
  return (
    <AntLayout>
      <Header>
        <Navbar />
      </Header>
      <Content className="site-layout-content">
        <div className="max-w-7xl mx-auto">
          <Outlet />
        </div>
      </Content>
      <Toaster position="top-right" />
    </AntLayout>
  );
}
