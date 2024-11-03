import { useEffect, useState } from "react";
import { Button, Form, Input, message, Modal, Select, Upload } from "antd";
import { UploadOutlined } from "@ant-design/icons";
import { api } from "../services/api";
import type { UploadFile } from "antd/es/upload/interface";
import type { RcFile } from "antd/es/upload";

const { TextArea } = Input;
const { Option } = Select;

interface UploadModalProps {
  isOpen: boolean;
  onClose: () => void;
  onUpload: (data: FormData) => Promise<void>;
}

export function UploadModal({ isOpen, onClose, onUpload }: UploadModalProps) {
  const [form] = Form.useForm();
  const [fileList, setFileList] = useState<UploadFile[]>([]);
  const [elements, setElements] = useState([]);

  useEffect(() => {
    fetchElements();
  }, []);

  const fetchElements = async () => {
    try {
      const response = await api.get("/Element");
      setElements(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleSubmit = async (values: {
    title: string | Blob;
    description: string | Blob;
    elementIds: string[];
  }) => {
    const formData = new FormData();
    formData.append("Title", values.title);
    formData.append("Description", values.description);
    values.elementIds.forEach((elementId: string) => {
      formData.append("ElementIds[]", elementId);
    });

    if (fileList[0] && fileList[0].originFileObj) {
      formData.append("ImageFile", fileList[0].originFileObj);
    }

    await onUpload(formData);
    form.resetFields();
    setFileList([]);
    onClose();
  };

  const beforeUpload = (file: RcFile) => {
    const isImage = file.type.startsWith("image/");
    if (!isImage) {
      message.error("You can only upload image files!");
    }
    return false;
  };

  return (
    <Modal
      open={isOpen}
      title="Share Your NPU Creation"
      onCancel={onClose}
      footer={null}
      width={640}
    >
      <Form
        form={form}
        layout="vertical"
        onFinish={handleSubmit}
        className="mt-4"
      >
        <Form.Item
          name="title"
          label="Title"
          rules={[{ required: true, message: "Please enter a title" }]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          name="description"
          label="Description"
          rules={[{ required: true, message: "Please enter a description" }]}
        >
          <TextArea rows={3} />
        </Form.Item>

        <Form.Item
          name="elementIds"
          label="Elements"
          rules={[{ required: true, message: "Please select the elements" }]}
        >
          <Select mode="multiple" placeholder="Please select elements">
            {elements.map((el: { elementId: string; name: string }) => (
              <Option key={el.elementId} value={el.elementId}>
                {el.name}
              </Option>
            ))}
          </Select>
        </Form.Item>

        <Form.Item
          name="image"
          label="Image"
          rules={[{ required: true, message: "Please upload an image" }]}
        >
          <Upload
            listType="picture-card"
            fileList={fileList}
            beforeUpload={beforeUpload}
            onChange={({ fileList }) => setFileList(fileList)}
            maxCount={1}
          >
            {fileList.length === 0 && (
              <div>
                <UploadOutlined />
                <div style={{ marginTop: 8 }}>Upload</div>
              </div>
            )}
          </Upload>
        </Form.Item>

        <Form.Item className="mb-0 text-right">
          <Button onClick={onClose} style={{ marginRight: 8 }}>
            Cancel
          </Button>
          <Button type="primary" htmlType="submit">
            Share Creation
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
}
