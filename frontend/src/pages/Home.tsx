import { useState, useEffect } from "react";
import { Input, Row, Col, Spin, Button } from "antd";
import { NPUCard } from "../components/NpuCard";
import { UploadModal } from "../components/UploadModal";
import { api } from "../services/api";
import type { NPUCreation, ApiResponse } from "../types";
import { PlusCircle } from "lucide-react";

const { Search: AntSearch } = Input;

export default function Home() {
  const [creations, setCreations] = useState<NPUCreation[]>([]);
  const [isUploadModalOpen, setIsUploadModalOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    fetchCreations();
  }, []);

  const fetchCreations = async () => {
    try {
      setIsLoading(true);
      const response = await api.get<ApiResponse<NPUCreation[]>>(
        "/NpuCreation"
      );
      setCreations(
        // @ts-expect-error (adjust in future)
        response.data.map((creation) => ({
          id: creation.id,
          title: creation.title,
          description: creation.description,
          imageUrl: creation.imageUrl,
          elementIds: creation.elementIds,
          createdAt: creation.createdAt,
          userId: creation.userId,
          totalScore: creation.totalScore,
        }))
      );
    } catch (error) {
      console.error(error);
      setCreations([]);
    } finally {
      setIsLoading(false);
    }
  };

  const handleUpload = async (formData: FormData) => {
    try {
      await api.post<ApiResponse<NPUCreation>>("/NpuCreation", formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });
      await fetchCreations();
      setIsUploadModalOpen(false);
    } catch (error) {
      console.error(error);
    }
  };

  const handleVote = async (creationId: string, value: number) => {
    try {
      await api.post<ApiResponse<void>>(`/Score`, { creationId, value });
      await fetchCreations();
    } catch (error) {
      console.error(error);
    }
  };

  const filteredCreations = creations.filter((creation) =>
    // TODO: figure out how to add search by elements used
    creation.title.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <>
      <div className="mb-8">
        <Button
          type="primary"
          icon={<PlusCircle size={18} />}
          onClick={() => setIsUploadModalOpen(true)}
        >
          New Creation
        </Button>
      </div>

      <div className="mb-8">
        <AntSearch
          placeholder="Search by element name or title..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          size="large"
        />
      </div>

      {isLoading ? (
        <div className="flex justify-center items-center h-64">
          <Spin size="large" />
        </div>
      ) : (
        <Row gutter={[16, 16]}>
          {filteredCreations.map((creation) => (
            <Col xs={24} sm={12} lg={8} key={creation.id}>
              <NPUCard creation={creation} onVote={handleVote} />
            </Col>
          ))}
        </Row>
      )}

      <UploadModal
        isOpen={isUploadModalOpen}
        onClose={() => setIsUploadModalOpen(false)}
        onUpload={handleUpload}
      />
    </>
  );
}
